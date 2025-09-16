import { createRouter, createWebHistory } from 'vue-router'
import NotesList from '../pages/NotesPage.vue'
import CreateNotePage from '../pages/CreateNotePage.vue'

const routes = [
  { path: '/', component: NotesPage },
  { path: '/create', component: CreateNotePage },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
