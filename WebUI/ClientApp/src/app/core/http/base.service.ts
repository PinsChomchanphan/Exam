import { Injectable } from '@angular/core';

import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http';


@Injectable({
    providedIn: 'root'
})
export class BaseService {

    constructor(public http: HttpClient) { }

    //getPosts(param) {
    //    return this.http.get(this.url + param);
    //}

    //createPost(post) {
    //    return this.http.post(this.url, JSON.stringify(post))
    //}

    //updatePost(post) {
    //    return this.http.patch(this.url + '/' + post.id, JSON.stringify({ isRead: true }))
    //}

    //deletePost(id) {
    //    return this.http.delete(this.url + '/' + id);
    //}
}
