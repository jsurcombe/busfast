import { Vue } from 'vue-class-component';

export class DateMixin extends Vue {
  timePart(d: string): string {
    return d.substring(10, 5);
  }
}