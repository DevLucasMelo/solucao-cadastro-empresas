<script setup>
    import { defineProps } from 'vue'

    const props = defineProps({
        estaLogado: Boolean,
        logout: Function
    })
</script>

<template>
    <nav class="navbar navbar-expand-lg navbar-light bg-light mb-4">
        <div class="container">
            <router-link class="navbar-brand" to="/">Cadastro de Empresas</router-link>

            <button class="navbar-toggler"
                    type="button"
                    data-bs-toggle="collapse"
                    data-bs-target="#navbarNav"
                    aria-controls="navbarNav"
                    aria-expanded="false"
                    aria-label="Alternar navega��o">
                <span class="navbar-toggler-icon"></span>
            </button>

            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                    <li class="nav-item">
                        <router-link class="nav-link" to="/"> Pagina inicial</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link class="nav-link" to="/empresas">Cadastrar empresas</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link class="nav-link" to="/consultar-empresas">Consultar empresas</router-link>
                    </li>
                </ul>

                <div class="d-flex">
                    <router-link v-if="!props.estaLogado"
                                 class="btn btn-outline-secondary me-2"
                                 to="/cadastro">
                        Cadastrar Usuario
                    </router-link>

                    <router-link v-if="!props.estaLogado"
                                 class="btn btn-outline-primary me-2"
                                 to="/login">
                        Login
                    </router-link>

                    <button v-else
                            class="btn btn-outline-danger"
                            @click="props.logout">
                        Logout
                    </button>
                </div>
            </div>
        </div>
    </nav>
</template>
