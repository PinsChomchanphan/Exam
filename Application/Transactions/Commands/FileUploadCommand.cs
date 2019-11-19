using Exam2C2P.Application.Common.Interfaces;
using Exam2C2P.Domain.Entities;
using Exam2C2P.Domain.Enums;
using MediatR;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Exam2C2P.Application.Transactions.Commands
{
    public class FileUploadCommand : IRequest
    {
        public Stream FileStream { get; set; }
        public string FileType { get; set; }
        public class FileUploadCommandHandler : IRequestHandler<FileUploadCommand>
        {
            private readonly IExamDatabaseDbContext _context;
            private readonly IMediator _mediator;
            private const string XML = "xml";
            private const string CSV = "csv";
            private List<string> CurrencyCodes = Enum.GetValues(typeof(CurrencyCodes))
                                       .Cast<CurrencyCodes>()
                                       .Select(v => v.ToString())
                                       .ToList();

            public FileUploadCommandHandler(IExamDatabaseDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(FileUploadCommand request, CancellationToken cancellationToken)
            {
                List<Transaction> res;

                switch (request.FileType)
                {
                    case "csv":
                        res = TransactionCSVReader(request.FileStream);
                        break;
                    case "xml":
                        res = TransactionXmlReader(request.FileStream);
                        break;
                    default:
                        throw new Exception("Unknown format");

                }
                if (res.Count != 0 )
                {
                    _context.Transactions.AddRange(res);
                    await _context.SaveChangesAsync(cancellationToken);
                }
              
                return Unit.Value;
            }
            private List<Transaction> TransactionCSVReader(Stream stream)
            {
                
                try
                {
                    List<Transaction> transactionList = new List<Transaction>();
                    TextFieldParser parser = new TextFieldParser(stream);
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    while (!parser.EndOfData)
                    {
                        //Processing row
                        string[] fields = parser.ReadFields();
                        var currencyCode = fields[2];
                        bool isCorrect = CurrencyCodes.Where(e => e == currencyCode).Count() == 1;
                       
                        var model = new Transaction()
                        {
                            TransactionId = fields[0],
                            Amount = decimal.Parse(Regex.Replace(fields[1], @"[^\d.]", "")),
                            CurrencyCode = isCorrect ? currencyCode : throw new Exception("The data is not correct"),
                            Created = DateTime.UtcNow,
                            FileType = CSV,
                            TransactionDate = DateTime.ParseExact(fields[3], "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                        Status = CheckStatus(CSV,fields[4])

                        };
                        transactionList.Add(model);
                    }
                    return transactionList;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                
            }
            private List<Transaction> TransactionXmlReader(Stream stream)
            {
                List<Transaction> transactionList = new List<Transaction>();
                try
                {
                    XmlReader reader = XmlReader.Create(stream);
                    var model = new Transaction();
                    while (reader.Read())
                    {
                        // Only detect start elements.
                        if (reader.IsStartElement())
                        {
                            // Get element name and switch on it.
                            switch (reader.Name)
                            {
                                case "Transactions":
                                    break;
                                case "Transaction":
                                    Transaction lastElement = transactionList.LastOrDefault();
                                    if (lastElement != null)
                                    {
                                        string tranId = reader["id"];
                                        if (!tranId.Equals(lastElement.TransactionId, StringComparison.CurrentCultureIgnoreCase))
                                        {
                                            model = new Transaction
                                            {
                                                TransactionId = reader["id"],
                                                FileType = XML,
                                                Created = DateTime.UtcNow
                                            };
                                            transactionList.Add(model);
                                        }
                                    }
                                    else
                                    {
                                        model = new Transaction
                                        {
                                            TransactionId = reader["id"],
                                            FileType = XML,
                                            Created = DateTime.UtcNow
                                        };
                                        transactionList.Add(model);
                                    }
                                    break;
                                case "TransactionDate":
                                    model.TransactionDate = Convert.ToDateTime(reader.ReadString());
                                    break;
                                case "PaymentDetails":
                                    break;
                                case "Amount":
                                    model.Amount = decimal.Parse(Regex.Replace(reader.ReadString(), @"[^\d.]", ""));
                                    break;
                                case "CurrencyCode":
                                    var currencyCode = reader.ReadString();
                                    bool isCorrect = CurrencyCodes.Where(e => e == currencyCode).Count() == 1;
                                    if (isCorrect)
                                        model.CurrencyCode = currencyCode;
                                    else
                                        throw new Exception("The data is not correct");
                                    break;
                                case "Status":
                                    model.Status = CheckStatus(XML, reader.ReadString());
                                    break;

                            }
                        }
                    }

                    return transactionList;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }


            private string CheckStatus(string type , string data)
            {
                string outStatus = string.Empty;
                if (type.Equals("csv", StringComparison.CurrentCultureIgnoreCase))
                {
                    switch (data)
                    {
                        case "Approved":
                            outStatus = "A";
                            break;
                        case "Failed":
                            outStatus = "R";
                            break;
                        case "Finished ":
                            outStatus = "D";
                            break;
                    }
                }
                else if (type.Equals("xml", StringComparison.CurrentCultureIgnoreCase))
                {
                    switch (data)
                    {
                        case "Approved":
                            outStatus = "A";
                            break;
                        case "Rejected":
                            outStatus = "R";
                            break;
                        case "Done":
                            outStatus = "D";
                            break;
                    }
                }
                return outStatus;

            }
        }
    }
}

