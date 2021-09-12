<template>
    <div id="nav">
        <router-link to="/">Home</router-link>
    </div>
    <router-view />
</template>

<script lang="ts">

</script>

<style>

    ul {
        list-style-type: none;
        padding: 0;
    }

    body {
        background: black
    }

    input {
        font-family: System, monospace;
    }

    #app {
        font-family: System, monospace;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: white;
    }

    #nav {
        padding: 30px;
    }

        #nav a {
            font-weight: bold;
            color: #2c3e50;
        }

            #nav a.router-link-exact-active {
                color: #42b983;
            }
</style>
