import { type IUser } from '../../shared/models/user.model';

export interface ILoginPayload {
  email: string;
  password: string;
}

export interface IloginResponse {
  data: IUser;
  token: string;
}

export interface ISignupPayload {
  username: string;
  email: string;
  password: string;
}

export interface ISignupResponse {
  data: IUser;
  token: string;
}

export interface IFetchUserResponse {
  data: IUser;
}
