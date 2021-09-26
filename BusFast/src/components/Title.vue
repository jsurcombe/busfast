<script lang="ts">
    import { Options, Vue } from "vue-class-component";
    import { Watch } from "vue-property-decorator";

    @Options({
        props: {
            title: String
        }
    })
    export default class Title extends Vue {

        @Watch('title', { immediate: true })
        titleChanged(title: string) {
            document.title = title;
        }
    }
</script>
