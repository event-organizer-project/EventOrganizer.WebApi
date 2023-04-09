import { useSelector } from 'react-redux'
import { Box, Grid, Typography, Button } from '@mui/material';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import EventTagList from 'components/EventTagList/EventTagList'
import UserView from 'components/UserView/UserView'
import { mapEnumToString } from 'mappers/recurrence-type-mapper'

export default function SpecificEventPageView({ event, toUpdateMode }) {

    const user = useSelector((state) => state.auth.user)

    return event && (
        <Box>
            <Typography variant="h5" gutterBottom>
                {event.title} 
                {event.owner.id == user.id && <Button onClick={toUpdateMode}>Update</Button>}
            </Typography>
            <Grid container spacing={2} width='100vw'>
                <Grid item xs={12} sm={5} width='100%' >

                    <Typography variant="subtitle2">Description:</Typography>
                    <Typography>{event.description}</Typography>

                    <Typography variant="subtitle2">Date:</Typography>
                    <Typography>{event.startDate}<AccessTimeIcon sx={{ height: "0.5em", width: '0.5em', ml: '0.5em' }} /> {event.startTime} - {event.endTime}</Typography>

                    <Typography variant="subtitle2">Recurrence:</Typography>
                    <Typography>{mapEnumToString(event.recurrenceType)}</Typography>

                    <Typography variant="subtitle2">Tags:</Typography>
                    <EventTagList tags={event.eventTags} />

                    <Typography variant="subtitle2">Owner:</Typography>
                    <UserView user={event.owner} />

                </Grid>

                <Grid item xs={12} sm={5}>
                </Grid>
            
            </Grid>
        </Box>
    )
}