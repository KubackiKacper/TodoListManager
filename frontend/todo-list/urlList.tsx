interface IURLProps {
  [key: string]: {
    urlLink: string;
  };
}

const apiUrls: IURLProps = {
  toDoListUrl: {
    urlLink: "https://localhost:7213/todo/assignments",
  },
  
};

export default apiUrls;