import { ref, computed } from 'vue'

const token = ref(localStorage.getItem('token'))

export function useAuth() {
    function loginUser(novoToken) {
        localStorage.setItem('token', novoToken)
        token.value = novoToken
    }

    function logoutUser() {
        localStorage.removeItem('token')
        token.value = null
    }

    const estaLogado = computed(() => !!token.value)

    return { token, estaLogado, loginUser, logoutUser }
}
