'use client'
import React, { useEffect, useState } from 'react'
import styles from './ToDoList.module.css'
import apiUrls from '@/urlList'
import { FaTrash } from "react-icons/fa6";
import { FaRegEdit } from "react-icons/fa";
export interface IApiDataProps{
  id:number,
  description:string
}

const ToDoList = () => {
  const [apiData, setApiData] = useState<IApiDataProps[]>([])
  
  useEffect(()=>
    {
      fetchFromApi()
    },[]
    )

  const fetchFromApi = async () =>{
    try{
      const response =  await fetch(apiUrls.toDoListUrl.urlLink)
      const data = await response.json();
      setApiData(data)
      }
    catch(error)
    {
      console.error("Failed while fetching data")
    }
  }
  
  return (
    <>
    <div>
      
      <ul className={styles.list}>
        {apiData.map((item) => (
          
          <li 
            key={item.id} 
            className={styles.listItem} 
          >
            {item.description}
            <button className={styles.deleteButton}><FaTrash /></button>
            <button className={styles.editButton}><FaRegEdit /></button>
          </li>
          
        ))}
      </ul>
      
    </div>
    </>
  )
}

export default ToDoList