// import axios from 'axios';

// const api = axios.create({
// //   baseURL: 'https://localhost:5001/api', 
//   baseURL: 'http://localhost:5258/api', 
//   headers: {
//     'Content-Type': 'application/json'
//   }
// });

// export default api;

// src/api/axios.ts
import axios from 'axios'

const api = axios.create({
  baseURL: 'https://localhost:5001/api', // change this to your backend URL
  headers: {
    'Content-Type': 'application/json'
  }
})

export default api
