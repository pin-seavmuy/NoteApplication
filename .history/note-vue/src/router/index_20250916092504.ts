import { createRouter, createWebHistory } from 'vue-router'

import NotesList from '../components/NotesList.vue'
import CreateNote from '../components/CreateNote.vue'
import NoteDetail from '../components/NoteDetail.vue'
import UpdateNote from '@/components/UpdateNote.vue'

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
    {
    path: '/notes/:id/edit',
    name: 'EditNote',
    component: UpdateNote,
    props: true
    }

]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
