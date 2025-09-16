import { createRouter, createWebHistory } from 'vue-router'
import NotesList from '@/views/NotesList.vue'
import NoteDetails from '@/views/NoteDetails.vue'

const routes = [
  { path: '/', name: 'NotesList', component: NotesList },
  { path: '/note/:id', name: 'NoteDetails', component: NoteDetails },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
