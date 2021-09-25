<template>
    <div id="nav">
        <router-link :to="{ name: 'Home' }">[Home]</router-link>
        <router-link :to="{ name: 'Clusters' }">[Stops]</router-link>
        <router-link :to="{ name: 'Journey' }">[Journey Planner]</router-link>

        <div v-if="$route.name == 'Home'" style="margin-top: 10px">
            <img src="@/assets/bus.png" style="width: 204px; height: 112px; image-rendering: crisp-edges; vertical-align:middle" />
        </div>
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
        color: #8da;
    }

    #nav {
        position: relative;
        margin: 0;
        padding: 10px;
        background: #333;
        margin-bottom: 10px;
    }

        #nav a {
            margin-left: 0.5em;
            margin-right: 0.5em;
        }

    table {
        margin-left: auto;
        margin-right: auto;
        text-align: left;
    }
</style>
