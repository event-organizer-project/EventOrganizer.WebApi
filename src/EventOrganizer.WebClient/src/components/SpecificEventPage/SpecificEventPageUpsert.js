import { Box, Grid, Typography } from '@mui/material';
import { useHistory } from "react-router-dom";
import EventUpsertForm from '../EventUpsertForm/EventUpsertForm'
import eventRequestService from 'services/eventRequestService'
import routes from 'constants/route-constants'

export default function SpecificEventPageUpsert({ event, toViewMode, setEvent }) {
    
    const isCreateMode = !event;
    const history = useHistory();

    const onSubmit = isCreateMode 
        ? (data) => eventRequestService.post(data)
            .then(result => { 
                history.push(`${routes.events}/${result.id}`);
                setEvent(result);
                toViewMode();
            })
        : (data) => eventRequestService.put(data)
            .then(result => { 
                setEvent(result);
                toViewMode();
            });

    return (
        <Box>
            <Typography variant="h5" gutterBottom>
                {isCreateMode ? 'Crete' : 'Update'} event page
            </Typography>
            <Grid container spacing={2} width='100vw'>
                <Grid item xs={12} sm={4} width='100%' >
                    <EventUpsertForm event={event ? event : emptyEvent} submitAction={onSubmit} cancelAction={toViewMode}/>
                </Grid>
                <Grid item xs={12} sm={8}>
                </Grid>
            </Grid>
        </Box>
    )
}

const emptyEvent = {
    title: '',
    description: '',
    startDate: null,
    endDate: null,
    startTime: null,
    endTime: null,
    eventTags: [],
    members: [],
}