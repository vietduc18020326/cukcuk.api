import axios from 'axios'
import {apiUrl} from '../config.json'

const api = apiUrl + 'position';

const position = axios.get(api)
    .then(res => {
        return res;
    });

const getPosition = async () => {
    let res = await position;
    return res.data;
}

export default {
    getPosition
}