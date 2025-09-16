import { createRouter, createWebHistory } from 'vue-router'

import NotesList from '../components/NotesList.vue'
import CreateNote from '../components/CreateNote.vue'
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
  {
    path: '/notes/:id',
    name: 'NoteDetail',
    component: NoteDetail,
    props: true,
  },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
