import { createRouter, createWebHistory } from 'vue-router'

import NotesList from '../views/NotesList.vue'
import NoteDetail from '../views/NoteDetail.vue'
import EditNoteModal from '../components/EditNoteModal.vue'

const routes = [
    {
        path: '/',
        name: 'NotesList',
        component: NotesList,
    },
    {
        path: '/notes/:id',
        name: 'NoteDetail',
        component: NoteDetail,
        props: true,
    },
    {
        path: '/notes/:id/edit',
        name: 'UpdateNote',
        component: EditNoteModal,
        props: true
    }
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
