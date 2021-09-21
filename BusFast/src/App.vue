<template>
    <div id="nav">
        <router-link :to="{ name: 'Home' }">[Home]</router-link>
        <router-link :to="{ name: 'Clusters' }">[Stops]</router-link>
        <router-link :to="{ name: 'Journey' }">[Journey Planner]</router-link>
    </div>
    <router-view />
</template>

<script lang="ts">

</script>

<style>

    ul.suggestions {
        background-color: white;
        color: black;
        position: absolute;
        left: 0;
        right: 0;
    }

    div.autocomplete {
        display: inline-block;
        position: relative;
    }

    ul.suggestions {
        margin: 0;
        z-index: 1;
    }

    .suggestions > li:hover {
        background-color: #ddd
    }

    ul {
        list-style-type: none;
        padding: 0;
    }

    li {
        padding: 0.2em;
    }

        li a {
            display: block;
            padding: 0.2em;
        }

    .highlight {
        font-weight: bold;
        background-color: #42b983;
        color: black;
    }

        .highlight a {
            color: black;
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
            margin-left: 0.5em;
            margin-right: 0.5em;
        }

            #nav a .router-link-exact-active {
                color: #42b983;
            }

    table {
        margin-left: auto;
        margin-right: auto;
        text-align: left;
    }
</style>
