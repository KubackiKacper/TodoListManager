import React from 'react';
import { Bounce, toast } from 'react-toastify';

export enum TypeEnum {
  success,
  info,
  warn,
  error,
}

interface INotifyProps {
  type: TypeEnum;
  message: string | any;
}
const defaultOptions = {
  position: 'top-left' as const,
  autoClose: 3000,
  hideProgressBar: false,
  closeOnClick: true,
  pauseOnHover: true,
  draggable: true,
  theme: 'dark' as const,
  transition: Bounce,
};

const notify = ({ type, message }: INotifyProps) => {
  switch (type) {
    case TypeEnum.info:
      toast.info(message, defaultOptions);
      break;
    case TypeEnum.success:
      toast.success(message, defaultOptions);
      break;
    case TypeEnum.warn:
      toast.warn(message, defaultOptions);
      break;
    case TypeEnum.error:
      toast.error(message, defaultOptions);
      break;
    default:
      toast(message, defaultOptions);
      break;
  }
};

export default notify;
