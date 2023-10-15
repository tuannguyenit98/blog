import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BaseService {
  constructor(
    public http: HttpClient,
    @Inject('API_BASE_URL') public baseUrl: string,
) {}

public checkTime(time: number, url: string) {
    if (time > 200) {
        console.warn(
            '[Response Time Warning]',
            url,
            'Response Time: ' + time,
        );
    } else {
        console.info('[Response Time Info]', url, 'Response Time: ' + time);
    }
}

public get<T>(url: string, params?: any, headers?: any): Observable<any> {
    return this.http
        .get(this.baseUrl + url, {
            params,
            headers: { 'Content-Type': 'application/json' },
            observe: 'response',
        })
        .pipe(
            map((res) => {
                // this.checkTime(
                //     parseInt(
                //         res.headers.get('X-Response-Time-Milliseconds') ||
                //             '{}'
                //     ),
                //     res.url || '{}'
                // );
                return (res.body as any).data;
            }),
        );
}

public post<T>(url: string, data?: any, header?: any): Observable<any> {
    return this.http
        .post(this.baseUrl + url, data, {
            headers: { 'Content-Type': 'application/json' },
            observe: 'response',
            withCredentials: true,
        })
        .pipe(
            map((res) => {
                // this.checkTime(
                //     parseInt(
                //         res.headers.get('X-Response-Time-Milliseconds') ||
                //             '{}'
                //     ),
                //     res.url || '{}'
                // );
                return res.status === 201 || res.status === 204
                    ? []
                    : (res.body as any).data;
            }),
        );
}

public put<T>(url: string, data?: any, headers?: any): Observable<any> {
    return this.http
        .put(this.baseUrl + url, data, {
            headers: { 'Content-Type': 'application/json' },
            observe: 'response',
        })
        .pipe(
            map((res) => {
                this.checkTime(
                    parseInt(
                        res.headers.get('X-Response-Time-Milliseconds') ||
                            '{}',
                    ),
                    res.url || '{}',
                );
                return res.status === 201 || res.status === 204
                    ? []
                    : (res.body as any).data;
            }),
        );
}

public patch<T>(url: string, data?: any, headers?: any): Observable<any> {
    return this.http
        .patch(this.baseUrl + url, data, {
            headers: { 'Content-Type': 'application/json' },
            observe: 'response',
        })
        .pipe(
            map((res) => {
                // this.checkTime(
                //     parseInt(
                //         res.headers.get('X-Response-Time-Milliseconds') ||
                //             '{}'
                //     ),
                //     res.url || '{}'
                // );
                return res.status === 201 || res.status === 204
                    ? []
                    : (res.body as any).data;
            }),
        );
}

public delete<T>(url: string, data?: any, headers?: any): Observable<any> {
    return this.http
        .delete<T>(this.baseUrl + url, {
            headers: { 'Content-Type': 'application/json' },
            observe: 'response',
        })
        .pipe(
            map((res) => {
                // this.checkTime(
                //     parseInt(
                //         res.headers.get('X-Response-Time-Milliseconds') ||
                //             '{}'
                //     ),
                //     res.url || '{}'
                // );
                return res.status === 201 || res.status === 204
                    ? []
                    : (res.body as any).data;
            }),
        );
}

public deleteArray<T>(
    url: string,
    data?: any,
    headers?: any,
): Observable<any> {
    return this.http
        .request<T>('delete', this.baseUrl + url, {
            headers: { 'Content-Type': 'application/json' },
            body: data,
            observe: 'response',
        })
        .pipe(
            map((res) => {
                // this.checkTime(
                //     parseInt(
                //         res.headers.get('X-Response-Time-Milliseconds') ||
                //             '{}'
                //     ),
                //     res.url || '{}'
                // );
                return res.status === 201 || res.status === 204
                    ? []
                    : (res.body as any).data;
            }),
        );
}
}
