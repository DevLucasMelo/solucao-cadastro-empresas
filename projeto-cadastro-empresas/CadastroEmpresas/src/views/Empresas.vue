<template>
    <div class="container mt-4">
        <h1 class="mb-4">Empresas</h1>

        <form @submit.prevent="cadastrarEmpresa" class="mb-4">
            <div class="input-group">
                <input v-model="cnpj"
                       type="text"
                       class="form-control"
                       placeholder="Digite o CNPJ (somente numeros)"
                       required
                       aria-label="CNPJ" />
                <button type="submit"
                        class="btn btn-primary"
                        :disabled="!cnpj || !cnpj.trim()">
                    Cadastrar
                </button>
            </div>
        </form>

        <div class="mb-4">
            <router-link to="/consultar-empresas" class="btn btn-success">
                Ver listagem com todas empresas cadastradas
            </router-link>
        </div>

        <div v-if="mensagem"
             :class="erro ? 'alert alert-danger' : 'alert alert-success'"
             role="alert">
            {{ mensagem }}
        </div>
    </div>
</template>

<script setup>
    import { ref } from 'vue'
    import { fetchWithAuth } from '../services/api.js' 

    const cnpj = ref('')
    const mensagem = ref('')
    const erro = ref(false)

    const API_URL = import.meta.env.VITE_API_URL + '/empresas'

    async function cadastrarEmpresa() {
        try {
            if (!cnpj.value || !cnpj.value.trim()) {
                mensagem.value = 'Por favor, informe um CNPJ valido.'
                erro.value = true
                return
            }

            const response = await fetchWithAuth(`${API_URL}/cadastrar`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ cnpj: cnpj.value.trim() }),
            })

            if (!response.ok && response.json) {
                const data = await response.json()
                mensagem.value = data.message || 'Erro ao cadastrar empresa.'
                erro.value = true
                return
            }

            mensagem.value = 'Empresa cadastrada com sucesso.'
            erro.value = false
            cnpj.value = ''
        } catch (error) {
            erro.value = true
            mensagem.value = error.message || 'Erro ao cadastrar empresa.'
        }
    }
</script>
