import { IProduct } from "./product";

export interface IPagination {
    pageIndex?: number;
    pageSize?: number;
    countt?: number;
    data: IProduct[];
}