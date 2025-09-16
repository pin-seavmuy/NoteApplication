import { createRouter, createWebHistory } from 'vue-router'
import NotesList from '../pages/NotesPage.vue'
import NoteList from '../components/NotesList.vue'

const routes = [
  { path: '/', component: NotesList },
  { path: '/create', component: CreateNotePage },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
