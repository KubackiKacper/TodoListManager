interface IURLProps {
  [key: string]: {
    urlLink: string;
  };
}

const apiUrls: IURLProps = {
  toDoApiUrl: {
    urlLink: "https://localhost:7213/todo",
  },
};

export default apiUrls;