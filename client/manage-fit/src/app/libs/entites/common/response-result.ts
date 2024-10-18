export interface ResponseResult<T>{
    isSuccess: boolean;
    message: string;
    value: T;
}