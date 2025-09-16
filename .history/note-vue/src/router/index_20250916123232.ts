import { createRouter, createWebHistory } from 'vue-router'

import NotesList from '../views/NotesList.vue'
import CreateNote from '../views/CreateNote.vue'
import NoteDetail from '../views/NoteDetail.vue'
import UpdateNote from '../views/UpdateNote.vue'

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
  path: '/note/:id',
  name: 'NoteDetail',
  component: () => import('../views/NoteDetail.vue'),
}

    {
    path: '/notes/:id/edit',
    name: 'UpdateNote',
    component: UpdateNote,
    props: true
    }
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
