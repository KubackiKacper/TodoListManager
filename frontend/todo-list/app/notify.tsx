import React from 'react'
import { Bounce, toast } from 'react-toastify'

interface INotifyProps
{
  type:string,
  message:string,
  
}
const defaultOptions = {
  position: "top-left" as const,
  autoClose: 4000,
  hideProgressBar: false,
  closeOnClick: true,
  pauseOnHover: true,
  draggable: true,
  theme: "dark" as const,
  transition: Bounce,
};

const notify = ({type,message}:INotifyProps) => {
  switch(type)
  {
    case "info":
      toast.info(message,defaultOptions)
      break;
    case "success":
      toast.success(message,defaultOptions)
      break;
    case "warrning":
      toast.warn(message,defaultOptions)
      break;
    case "error":
      toast.error(message,defaultOptions)
      break;  
    default:
      toast(message,defaultOptions)
      break;
  }
}

export default notify