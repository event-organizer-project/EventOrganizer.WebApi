import { useSelector } from 'react-redux'
import { CircularProgress } from '@mui/material'

export default function LoadingIndicator () {

    const loading = useSelector((state) => state.general.loading)

    const indicatorSize = 80;

    const styles = {
        position: 'absolute',
        top: '50%',
        left: '50%',
        marginTop: `${-indicatorSize/2}px`,
        marginLeft: `${-indicatorSize/2}px`,
        color: 'success'
      }

    return loading && <CircularProgress size={indicatorSize}
        color="success"
        sx={styles}/>
}