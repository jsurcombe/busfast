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

    li {
        line-height: 1.5em;
    }

    body {
        font-size: 1.2em;
        background: black;
    }

    input {
        font-size: 1.2em;
        font-family: monospace;
        text-align: center;
    }

    #app {
        font-family: monospace;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: white;
    }


    a {
        color: #42b983;
    }

    #nav {
        padding: 30px;
    }

    #nav a {
        color: #2c3e50;
    }

    #nav a .router-link-exact-active {
        color: #42b983;
    }
</style>
