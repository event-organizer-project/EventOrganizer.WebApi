import { Link } from 'react-router-dom'
import { Box, Card, CardActionArea } from '@mui/material'
import EventTagList from '../EventTagList/EventTagList'
import routes from '../../constants/route-constants'

export default function EventView ({ event }) {

    const eventViewSX = {
        boxShadow: 1,
        borderRadius: 3,
        p: 1,
        maxWidth: 300,
        height: 120,
        margin: "auto"
    }

    const descriptionSX = {
        lineHeight: '1.5em',
        height: '3em',
        overflow: 'hidden'
    }

    const titleSX = { fontWeight: 'bold' }

    return (
        <Card sx={eventViewSX}>
            <CardActionArea href={`${routes.events}/${event.id}`}>
            <Box sx={titleSX}>{event.title}</Box>
            <Box sx={descriptionSX}>{event.description}</Box>
            <EventTagList tags={event.eventTags} />
            </CardActionArea>
            <Link to={`${routes.events}/${event.id}`}>To event details</Link>
        </Card>
    )
}