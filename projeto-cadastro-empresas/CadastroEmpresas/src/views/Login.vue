<script setup>
    import { ref } from 'vue'
    import { useRouter } from 'vue-router'

    const router = useRouter()

    const email = ref('')
    const senha = ref('')
    const erro = ref(null)
    const carregando = ref(false)
    const API_URL = import.meta.env.VITE_API_URL;

    async function login() {
        erro.value = null
        carregando.value = true
        try {
            const res = await fetch(`${API_URL}/usuarios/login`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ Email: email.value, Senha: senha.value })
            })

            if (!res.ok) {
                const data = await res.json()
                erro.value = data || 'Erro ao tentar fazer login.'
                carregando.value = false
                return
            }

            const data = await res.json()
            localStorage.setItem('token', data.token || data.Token)
            router.push('/')

        } catch (e) {
            erro.value = 'Erro na conexão com o servidor.'
        } finally {
            carregando.value = false
        }
    }
</script>

<template>
    <div class="container mt-5" style="max-width: 400px;">
        <h2 class="mb-4 text-center">Login</h2>
        <form @submit.prevent="login" novalidate>
            <div class="mb-3">
                <label for="email" class="form-label">Email:</label>
                <input id="email"
                       v-model="email"
                       type="email"
                       class="form-control"
                       required
                       autocomplete="username"
                       placeholder="Digite seu email" />
            </div>
            <div class="mb-3">
                <label for="senha" class="form-label">Senha:</label>
                <input id="senha"
                       v-model="senha"
                       type="password"
                       class="form-control"
                       required
                       autocomplete="current-password"
                       placeholder="Digite sua senha" />
            </div>
            <button type="submit"
                    class="btn btn-primary w-100"
                    :disabled="carregando">
                {{ carregando ? 'Entrando...' : 'Entrar' }}
            </button>
        </form>
        <div v-if="erro" class="alert alert-danger mt-3" role="alert">
            {{ erro }}
        </div>
        <div class="text-center mt-3">
            <small>
                Nao tem conta?
                <a href="#" @click.prevent="router.push('/cadastro')">Cadastre-se</a>
            </small>
        </div>
    </div>
</template>
