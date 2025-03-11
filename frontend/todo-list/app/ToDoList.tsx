'use client'
import React, {useState } from 'react'
import styles from './ToDoList.module.css'
import { FaTrash } from "react-icons/fa6";
import { FaRegEdit,FaRegSave  } from "react-icons/fa";
import { MdOutlineCancel } from "react-icons/md";
import { IToDoActions } from './ToDoForm';


export type ToDoProps ={
  id:number,
  description:string,
  completionStatus:boolean;
}

const ToDoList = (props:IToDoActions) => {
  
  const [isDialogOpen, setIsDialogOpen] = useState<boolean>(false);
  const [editedItem, setEditedItem] = useState<ToDoProps>();
  
  const handleCancel = () => {
    setIsDialogOpen(false);
  };

  const handleCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setEditedItem((prev) => {
      if (!prev) return prev;
      return { ...prev, completionStatus: event.target.checked };
    });
  };

  return (
    <>
    <div>
      <dialog 
        className={styles.dialogBox} 
        open={isDialogOpen}>
            <h2>Edit Task</h2>
            <input
                type="text"
                value={editedItem?.description ?? ""}
                onChange={(e) => setEditedItem({id:editedItem!.id,description:e.target.value,completionStatus:e.target.checked})}
                className={styles.dialogBoxInput}
            />
            <div>
              Completion Status:
              <input 
                type="checkbox"
                checked={editedItem?.completionStatus ??false}
                onChange={handleCheckboxChange}
                className={styles.statusCheckBox}>
              </input>
            </div>
            <div>
              <button
                type='button' 
                onClick={() => {
                  props.onEdit(editedItem!);
                  setIsDialogOpen(false);
                }} 
                className={styles.saveButton}>
                  <FaRegSave />
              </button>
              <button 
                type='button' 
                onClick={handleCancel} 
                className={styles.closeButton}>
                <MdOutlineCancel />
              </button>
            </div>
          
      </dialog>
      <ul className={styles.list}>
        {props.items.map((item) => (
          
          <li 
            key={item.id} 
            className={styles.listItem} 
          >
            {item.description}
            <button  
              type='button' 
              className={styles.deleteButton} 
              onClick={() => props.onDelete(item.id)}>
                <FaTrash />
            </button>
            <button  
              type='button' 
              className={styles.editButton} 
              onClick={(e) => {
                setEditedItem(item);
                setIsDialogOpen(true);
              }}>
              <FaRegEdit />
            </button>
          </li>
        ))}
      </ul>
    </div>
    </>
  )
}

export default ToDoList
