import { createRouter, createWebHistory } from 'vue-router'
import NotesList from '@/pages/NotesList.vue'

const routes = [
  {
    path: '/',
    name: 'Notes',
    component: NotesList
  },
  // Add more routes later for create/edit
]

const router = createRouter({
  history: createWebHistory(),import { createRouter, createWebHistory } from 'vue-router'
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

  routes
})

export default router
