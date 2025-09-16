import axios from 'axios'

const apiClient = axios.create({
  baseURL: 'http://localhost:5258/api',  // <-- your backend API base URL here
  headers: {
    'Content-Type': 'application/json',
  },
})

export default apiClient
