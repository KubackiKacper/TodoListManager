'use client'
import React, { useEffect, useState } from 'react'
import styles from './ToDoList.module.css'
import apiUrls from '@/urlList'
import { FaTrash } from "react-icons/fa6";
import { FaRegEdit,FaRegSave  } from "react-icons/fa";
import notify from './notify';
import { MdOutlineCancel } from "react-icons/md";


export interface IApiDataProps{
  id:number,
  description:string
}
export const timeout = (delay: number)=>{
  return new Promise( res => setTimeout(res, delay) );
}
const ToDoList = () => {
  const [apiData, setApiData] = useState<IApiDataProps[]>([])
  const [isDialogOpen, setIsDialogOpen] = useState<boolean>(false);
  const [editedNote, setEditedNote] = useState<IApiDataProps | null>(null);
  const [editedDescription, setEditedDescription] = useState<string>('');
  
  
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
  const handleEditClick = (item: IApiDataProps) => {
    setEditedNote(item);
    setEditedDescription(item.description);
    setIsDialogOpen(true);
  };
  const handleCancelClick = () => {
    setIsDialogOpen(false);
  };
  const handleDelete = async (id:number) => {
    try {
          const response = await fetch(`${apiUrls.deleteToDoItemUrl.urlLink}${id}`, { 
            method: 'DELETE'
        });

        if (response.ok) {
          notify({type:"warn",message:"Item deleted successfully!"});
          fetchFromApi();
            
        } else {
          notify({type:"error",message:"Could not delete the item."});
        }
    } catch (error) {
        console.error("Wystąpił błąd:", error);
    }
  };
  const handleSaveEdit = async () => {
    if (!editedNote) return;

    try {
        const response = await fetch(`${apiUrls.updateToDoItemUrl.urlLink}${editedNote.id}`, {
            method: 'PUT',
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ description: editedDescription })
        });

        if (response.ok) {
            notify({ type: "info", message: "Note updated successfully!" });
            setIsDialogOpen(false);
            
            fetchFromApi(); 
        } else {
            notify({ type: "error", message: "Could not update the item." });
        }
    } catch (error) {
        console.error("Wystąpił błąd:", error);
    }
  };

  return (
    <>
    
    <div>
      <dialog className={styles.dialogBox} open={isDialogOpen}>
          <h2>Edit Task</h2>
          <input
              type="text"
              value={editedDescription}
              onChange={(e) => setEditedDescription(e.target.value)}
              className={styles.input}
          />
          <div>
            <button type='button' onClick={handleSaveEdit} className={styles.saveButton}><FaRegSave /></button>
            <button type='button' onClick={handleCancelClick} className={styles.closeButton}><MdOutlineCancel /></button>
          </div>
          
      </dialog>
      <ul className={styles.list}>
        {apiData.map((item) => (
          
          <li 
            key={item.id} 
            className={styles.listItem} 
          >
            {item.description}
            <button  type='button' className={styles.deleteButton} onClick={()=>handleDelete(item.id)}><FaTrash /></button>
            <button  type='button' className={styles.editButton} onClick={() => handleEditClick(item)}><FaRegEdit /></button>
            
          </li>
          
        ))}
        
      </ul>
      
    </div>
    </>
  )
}

export default ToDoList
