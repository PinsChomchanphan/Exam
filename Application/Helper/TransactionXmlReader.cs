using Exam2C2P.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Exam2C2P.Application.Helper
{
    public class TransactionXmlReader
    {
        public List<Transaction> Read(Stream stream)
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
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
                                // Detect this article element.
                                Console.WriteLine("Start <article> element.");
                                // Search for the attribute name on this current node.
                                string attribute = reader["id"];
                                
                                break;
                            case "TransactionDate":
                                // Detect this element.
                                Console.WriteLine("TransactionDate.");
                                break;
                            case "PaymentDetails":
                                // Detect this element.
                                Console.WriteLine("PaymentDetails.");
                                break;
                            case "Amount":
                                // Detect this element.
                                Console.WriteLine("Amount.");
                                break;
                            case "CurrencyCode":
                                // Detect this element.
                                Console.WriteLine("CurrencyCode.");
                                break;
                        }
                    }
                }
            }
            return null; 
        }
    }
}
