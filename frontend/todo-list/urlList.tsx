interface IURLProps {
  [key: string]: {
    urlLink: string;
  };
}

const apiUrls: IURLProps = {
  toDoListUrl: {
    urlLink: "https://localhost:7213/todo/assignments",
  },
  deleteToDoItemUrl:{
    urlLink: "https://localhost:7213/todo/assignments/delete?id="
  },
  addToDoItemUrl:{
    urlLink: "https://localhost:7213/todo/assignments/add"
  },
  updateToDoItemUrl:{
    urlLink: "https://localhost:7213/todo/assignments/update/"
  }
};

export default apiUrls;