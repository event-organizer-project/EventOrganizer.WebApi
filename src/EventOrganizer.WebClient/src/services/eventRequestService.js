import axios from 'axios'
import RequestService from './requestService'
import { startLoading, finishLoading, setError } from 'store/generalSlice'
import store from 'store/store';

export class EventRequestService extends RequestService {

    constructor() {
        super('event');
    }

    schedule = (id, isScheduled) => {
        store.dispatch(startLoading());
        
        return axios
            .get(`${this.resourceName}/schedule/${id}/${isScheduled}`)
            .then(response => {
                return response.data;
            })
            .catch(error => {
                console.log('Error:', error);
                store.dispatch(setError(error.response.statusText));
            })
            .finally(() => {
                store.dispatch(finishLoading());
            });
    }
}

const eventRequestService = new EventRequestService();

export default eventRequestService;
