import { useState, useEffect } from 'react';
import { Box, Grid, ButtonGroup, Button } from '@mui/material'
import EventView from '../EventView/EventView'

export default function EventsGrid({ events, itemsPerPageCount, height }) {

    const [page, setPage] = useState({
        items: [],
        currentPage: 0,
        pageCount: 0
    })

    useEffect(() => {
        setPage({
            items: events.slice(0, itemsPerPageCount),
            currentPage: 1,
            pageCount: Math.ceil(events.length / itemsPerPageCount)
        })
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
        <Box display='flex' flexDirection='column' justifyContent='space-between' height={height}>
            <Grid container spacing={{ xs: 2, md: 3 }} columns={{ xs: 3, sm: 8, md: 12 }} style={{ width: '95vw' }}>
                {page.items.map(event => (
                    <Grid item xs={2} sm={4} md={4} key={event.id}>
                        <EventView event={event} />
                    </Grid>
                ))}
            </Grid>
            <ButtonGroup variant="text" aria-label="text button group" sx={{ margin: '1vh auto' }} >
                <Button onClick={previous} disabled={page.currentPage <= 1}>&lt;</Button>
                <Button disabled>{page.currentPage} / {page.pageCount}</Button>
                <Button onClick={next} disabled={page.currentPage == page.pageCount}>&gt;</Button>
            </ButtonGroup>
        </Box>
    )
}