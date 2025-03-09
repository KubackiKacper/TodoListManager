import styles from "./page.module.css";
import ToDoForm from "./ToDoForm";

export default function Home() {
  return (
    <div>
      <main className={styles.main}>
        TODO List
      </main>
      <ToDoForm/>
    </div>
  );
}
