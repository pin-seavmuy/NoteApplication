import { createRouter, createWebHistory } from 'vue-router'

import NotesList from '../components/NotesList.vue'
import CreateNote from '../components/CreateNote.vue'

const routes = [
  {
    path: '/',
    name: 'NotesList',
    component: NotesList,
  },
  {
    path: '/create',
    name: 'CreateNote',
    component: CreateNote,
  },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
