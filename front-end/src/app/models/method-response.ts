export interface MethodResponse {
  success: boolean;
  statusCode: number;
  errorCode?: number;
  message?: string;
  response?: any;
}
