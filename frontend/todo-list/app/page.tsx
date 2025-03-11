import { ToastContainer } from "react-toastify";
import styles from "./page.module.css";
import ToDoForm from "./ToDoForm";

export default function Home() {
  return (
    <div>
      <ToastContainer/>
      <main className={styles.main}>
        ToDo List
      </main>
      <ToDoForm/>
    </div>
  );
}
