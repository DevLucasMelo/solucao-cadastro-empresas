<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const nome = ref('')
const email = ref('')
const senha = ref('')
const confirmarSenha = ref('')
const erro = ref(null)
const carregando = ref(false)
const API_URL = import.meta.env.VITE_API_URL

async function cadastrar() {
  erro.value = null

  if (senha.value !== confirmarSenha.value) {
    erro.value = 'As senhas n�o coincidem.'
    return
  }

  carregando.value = true
  try {
    const res = await fetch(`${API_URL}/usuarios/register`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ Nome: nome.value, Email: email.value, Senha: senha.value }),
    })

    if (!res.ok) {
      const data = await res.json()
      erro.value = data || 'Erro ao cadastrar usu�rio.'
      carregando.value = false
      return
    }

    alert('Usu�rio cadastrado com sucesso! Fa�a login.')
    router.push('/login')
  } catch {
    erro.value = 'Erro na conex�o com o servidor.'
  } finally {
    carregando.value = false
  }
}
</script>

<template>
    <div class="container mt-5" style="max-width: 400px;">
        <h2 class="mb-4 text-center">Cadastro</h2>
        <form @submit.prevent="cadastrar" novalidate>
            <div class="mb-3">
                <label for="nome" class="form-label">Nome:</label>
                <input id="nome" v-model="nome" type="text" class="form-control" required placeholder="Digite seu nome completo" />
            </div>
            <div class="mb-3">
                <label for="email" class="form-label">Email:</label>
                <input id="email" v-model="email" type="email" class="form-control" required placeholder="Digite seu email" />
            </div>
            <div class="mb-3">
                <label for="senha" class="form-label">Senha:</label>
                <input id="senha" v-model="senha" type="password" class="form-control" required placeholder="Digite sua senha" />
            </div>
            <div class="mb-3">
                <label for="confirmarSenha" class="form-label">Confirmar Senha:</label>
                <input id="confirmarSenha" v-model="confirmarSenha" type="password" class="form-control" required placeholder="Confirme sua senha" />
            </div>
            <button type="submit" class="btn btn-primary w-100" :disabled="carregando">
                {{ carregando ? 'Cadastrando...' : 'Cadastrar' }}
            </button>
        </form>
        <div v-if="erro" class="alert alert-danger mt-3" role="alert">
            {{ erro }}
        </div>
    </div>
</template>
