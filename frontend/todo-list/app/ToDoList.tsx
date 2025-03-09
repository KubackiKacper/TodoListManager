'use client'
import React, { useEffect, useState } from 'react'
import styles from './ToDoList.module.css'
import apiUrls from '@/urlList'
import { FaTrash } from "react-icons/fa6";
import { FaRegEdit } from "react-icons/fa";
import notify from './notify';


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
  const handleDelete = async (id:number) => {
    try {
          const response = await fetch(`${apiUrls.deleteToDoItemUrl.urlLink}${id}`, { 
            method: 'DELETE'
        });

        if (response.ok) {
            alert("Usunięto pomyślnie!");
            // Możesz odświeżyć stan aplikacji np. filtrować listę
            
        } else {
            alert("Błąd podczas usuwania!");
        }
    } catch (error) {
        console.error("Wystąpił błąd:", error);
    }
};
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
            <button className={styles.deleteButton} onClick={()=>handleDelete(item.id)}><FaTrash /></button>
            <button className={styles.editButton} onClick={()=>notify({type:"success",message:"test"})}><FaRegEdit /></button>
          </li>
          
        ))}
      </ul>
      
    </div>
    </>
  )
}

export default ToDoList