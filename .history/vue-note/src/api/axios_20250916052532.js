import axios from 'axios'

const apiClient = axios.create({
  baseURL: 'http://localhost:5258/api', 
  headers: {
    'Content-Type': 'application/json',
  },
})

export default apiClient
