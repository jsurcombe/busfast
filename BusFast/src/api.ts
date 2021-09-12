import http from "@/http-common";
import { AxiosResponse } from "axios";

class Api {

  getStops(q: string): Promise<AxiosResponse<Stop[]>> {
    return http.get(`/stops`, { params: { q: q } });
  }

  getStop(id: number): Promise<AxiosResponse<StopView>> {
    return http.get(`/stops/${id}`);
  }

  getUpcomingServices(id: number): Promise<AxiosResponse<ServiceUpcoming[]>> {
    return http.get(`/services/upcoming`, { params: { stopId: id } });
  }
}

export default new Api();

export type Stop = {
  id: number;
  name: string;
}

export type StopView = {
  name: string;
}

export type RouteItem = {
  name: string;
}

export type ServiceUpcoming = {
  route: RouteItem;
  at: string;
}