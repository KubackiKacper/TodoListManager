'use client'
import React, { useState } from 'react'
import styles from "./ToDoForm.module.css";
import ToDoList, { IApiDataProps, timeout } from './ToDoList';
import apiUrls from '@/urlList';
import notify from './notify';

interface IToDoFormProps{
  description:string
}
const ToDoForm = () => {
  const [noteValue, setNoteValue] = useState<IToDoFormProps >({
    description:""
  })
  const [isDisabled, setIsDisabled] = useState<boolean>(false)

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    try{
      fetch(apiUrls.addToDoItemUrl.urlLink,{
        method:'POST',
        headers:{"Content-Type": "application/json"},
        body: JSON.stringify(noteValue)
      }).then(async ()=>{
        setIsDisabled(true)
        console.log(isDisabled)
        notify({type:"success",message:"Note added successfully!"});
        await timeout(3500)
        window.location.reload();
        
      })
    }
    catch(error)
    {
      console.error("Wystąpił błąd:", error);
    }
  }
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNoteValue({...noteValue,[e.target.name]:e.target.value})
    
  }
  return (
    <form onSubmit={handleSubmit}>
      <div className={styles.container}>
        <div className={styles.centered}>
          Add Task
          <div className={styles.input}>
            <input required type='text' name="description" onChange={handleChange} maxLength={255}></input>
            <button disabled={isDisabled} type='submit'>Submit</button>
          </div>
          <div className={styles.overflowDiv} style={{overflow: "auto",marginTop:"10px"}}>
            <ToDoList/>
            
          </div>
        </div>
        
      </div>
    </form>
  )
}

export default ToDoForm