import { ResponseResult } from "../common/response-result";

export interface GetClientResponse{
    name: string;
    weight: number,
    height: number,
    email: string,
    uid: string
}
export interface GetClientResult extends ResponseResult<GetClientResponse[]> {}