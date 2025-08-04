<style src="../assets/listagem.css" scoped></style>

<template>
    <div class="container-fluid mt-4 px-3">
        <h1 class="mb-4 text-center text-primary">Listagem de Empresas</h1>

        <div class="table-responsive">
            <table class="table table-striped fs-6 custom-striped">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Nome Empresarial</th>
                        <th scope="col">Nome Fantasia</th>
                        <th scope="col">CNPJ</th>
                        <th scope="col">Situação</th>
                        <th scope="col">Tipo</th>
                        <th scope="col">Abertura</th>
                        <th scope="col">Município</th>
                        <th scope="col">UF</th>
                        <th scope="col">Natureza Jurídica</th>
                        <th scope="col">Atividade Principal</th>
                        <th scope="col">Logradouro</th>
                        <th scope="col">Número</th>
                        <th scope="col">Complemento</th>
                        <th scope="col">Bairro</th>
                        <th scope="col">CEP</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(empresa, index) in empresas" :key="empresa.emp_id">
                        <th scope="row">{{ index + 1 + (page - 1) * pageSize }}</th>
                        <td>{{ empresa.emp_nome_empresarial }}</td>
                        <td>{{ empresa.emp_nome_fantasia }}</td>
                        <td>{{ empresa.emp_cnpj }}</td>
                        <td>{{ empresa.emp_situacao }}</td>
                        <td>{{ empresa.emp_tipo }}</td>
                        <td>{{ empresa.emp_abertura }}</td>
                        <td>{{ empresa.emp_municipio }}</td>
                        <td>{{ empresa.emp_uf }}</td>
                        <td>{{ empresa.emp_natureza_juridica }}</td>
                        <td>{{ empresa.emp_atividade_principal }}</td>
                        <td>{{ empresa.emp_logradouro }}</td>
                        <td>{{ empresa.emp_numero }}</td>
                        <td>{{ empresa.emp_complemento }}</td>
                        <td>{{ empresa.emp_bairro }}</td>
                        <td>{{ empresa.emp_cep }}</td>
                    </tr>
                </tbody>
            </table>
        </div>

        <nav aria-label="Paginação" class="mt-4">
            <ul class="pagination justify-content-center">
                <li class="page-item" :class="{ disabled: page === 1 }">
                    <button class="page-link" @click="mudarPagina(page - 1)" :disabled="page === 1">
                        Anterior
                    </button>
                </li>

                <li class="page-item" v-for="p in totalPages" :key="p" :class="{ active: p === page }">
                    <button class="page-link" @click="mudarPagina(p)">
                        {{ p }}
                    </button>
                </li>

                <li class="page-item" :class="{ disabled: page === totalPages }">
                    <button class="page-link" @click="mudarPagina(page + 1)" :disabled="page === totalPages">
                        Próximo
                    </button>
                </li>
            </ul>
        </nav>
    </div>
</template>

<script setup>
    import { ref, onMounted } from 'vue'
    import { fetchWithAuth } from '../services/api.js'

    const empresas = ref([])
    const page = ref(1)
    const pageSize = 10
    const totalPages = ref(1)

    const API_URL = import.meta.env.VITE_API_URL + '/empresas'

    async function listarEmpresas() {
        try {
            const url = `${API_URL}/listar?page=${page.value}&pageSize=${pageSize}`
            const response = await fetchWithAuth(url)
            empresas.value = response.empresas || []
            totalPages.value = response.totalPages || 1
        } catch (error) {
            console.error('Erro ao listar empresas:', error)
        }
    }

    function mudarPagina(novaPagina) {
        if (novaPagina >= 1 && novaPagina <= totalPages.value) {
            page.value = novaPagina
            listarEmpresas()
        }
    }

    onMounted(() => {
        listarEmpresas()
    })
</script>
