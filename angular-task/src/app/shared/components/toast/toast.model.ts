export interface Toast {
  id: number;
  message: string;
  type: 'success' | 'error' | 'info' | 'warn';
}
