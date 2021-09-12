import http from "@/http-common";
import { AxiosResponse } from "axios";

class Api {

  getStops(q: string): Promise<AxiosResponse<Stop[]>> {
    return http.get(`/stops`, { params: { q: q } });
  }

}

export default new Api();

export type Stop = {
  id: number;
  name: string;
}