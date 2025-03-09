'use client'
import React, { useState } from 'react'
import styles from "./ToDoForm.module.css";
import ToDoList, { IApiDataProps } from './ToDoList';

interface IToDoFormProps{
  description:string
}
const ToDoForm = () => {
  const [noteValue, setNoteValue] = useState<IToDoFormProps >({
    description:""
  })

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    console.log(noteValue)
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
            <button type='submit'>Submit</button>
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