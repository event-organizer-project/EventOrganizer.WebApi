import { useState, useEffect } from 'react';
import { Grid, ButtonGroup, Button } from '@mui/material'
import EventView from '../EventView/EventView'
import eventRequestService from '../../services/eventRequestService'

export default function AllEventsPage () {

    const itemsPerPageCount = 9;

    const [events, setEvents] = useState([]);
    const [page, setPage] = useState({
        items: [],
        currentPage: 0,
        pageCount: 0
    })

    const fetchEvents = () => {
        eventRequestService.getList(100, 0)
        .then(result => {
            setEvents(result);
            setPage({
                items: result.slice(0, itemsPerPageCount),
                currentPage: 1,
                pageCount: Math.ceil(result.length / itemsPerPageCount)
            })
        });
    }

    useEffect(() => {
        fetchEvents()
    }, [])

    const previous = (e) => {
        e.preventDefault();

        const newCurrentPage = page.currentPage - 1;
        const newPage = {
            items: events.slice((newCurrentPage - 1) * itemsPerPageCount, newCurrentPage * itemsPerPageCount),
            currentPage: newCurrentPage,
            pageCount: page.pageCount
        };

        setPage(newPage);
    }

    const next = (e) => {
        e.preventDefault();

        const newCurrentPage = page.currentPage + 1;
        const newPage = {
            items: events.slice(page.currentPage * itemsPerPageCount, newCurrentPage * itemsPerPageCount),
            currentPage: newCurrentPage,
            pageCount: page.pageCount
        };

        setPage(newPage);
    }

    return (
        <div>
            <h5>There will be a page with an event search panel and tools for creating new events.</h5>
            <Grid container spacing={{ xs: 2, md: 3 }} columns={{ xs: 3, sm: 8, md: 12 }} pl={15} my='auto'
            height='70vh'>
                {page.items.map(event => (
                    <Grid item xs={2} sm={4} md={4} key={event.id}>
                        <EventView event={event} />
                    </Grid>
                ))}
            </Grid>
            <ButtonGroup variant="text" aria-label="text button group" >
                <Button onClick={previous} disabled={page.currentPage <= 1}>&lt;</Button>
                <Button disabled>{page.currentPage} / {page.pageCount}</Button>
                <Button onClick={next} disabled={page.currentPage == page.pageCount}>&gt;</Button>
            </ButtonGroup>
        </div>
    )
}