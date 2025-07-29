<template>
    <div class="container mt-4">
        <h1 class="mb-4">Listagem de Empresas</h1>

        <table class="table table-striped table-bordered table-hover">
            <thead class="table-light">
                <tr>
                    <th>Nome Empresarial</th>
                    <th>Nome Fantasia</th>
                    <th>CNPJ</th>
                    <th>Situacao</th>
                    <th>Tipo</th>
                    <th>Abertura</th>
                    <th>Municipio</th>
                    <th>UF</th>
                    <th>Natureza Juridica</th>
                    <th>Atividade Principal</th>
                    <th>Logradouro</th>
                    <th>Numero</th>
                    <th>Complemento</th>
                    <th>Bairro</th>
                    <th>CEP</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="empresa in empresas" :key="empresa.emp_id">
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

        <nav aria-label="Pagina��o">
            <ul class="pagination justify-content-center">
                <li class="page-item" :class="{ disabled: page === 1 }">
                    <button class="page-link" @click="mudarPagina(page - 1)" :disabled="page === 1">
                        Anterior
                    </button>
                </li>

                <li class="page-item"
                    v-for="p in totalPages"
                    :key="p"
                    :class="{ active: p === page }">
                    <button class="page-link" @click="mudarPagina(p)">{{ p }}</button>
                </li>

                <li class="page-item" :class="{ disabled: page === totalPages }">
                    <button class="page-link"
                            @click="mudarPagina(page + 1)"
                            :disabled="page === totalPages">
                        Proximo
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
