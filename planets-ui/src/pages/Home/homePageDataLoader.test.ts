import { beforeEach, expect, vi } from 'vitest';
import createFetchMock from 'vitest-fetch-mock';
import { homePageDataLoader } from './homePageDataLoader';

const fetchMocker = createFetchMock(vi);
fetchMocker.enableMocks();

vi.stubEnv('VITE_API_URL', 'https://mock-api.com');

describe('homePageDataLoader', () => {
  describe('when the api responds successfully', () => {
    const planets = [
      { id: 'mars', name: 'Mars' },
      { id: 'earth', name: 'Earth' },
      { id: 'mercury', name: 'Mercury' },
    ];

    beforeEach(() => {
      fetchMocker.mockResponse(JSON.stringify(planets));
    });

    it('should call the correct api', async () => {
      await homePageDataLoader();

      expect(fetchMocker).toHaveBeenLastCalledWith('https://mock-api.com/Planets');
    })

    it('should return the deserialised planets', async () => {
      const result = await homePageDataLoader();

      expect(result.planets.length).toBe(planets.length);
      expect(result.planets).toEqual(expect.arrayContaining(planets));
    })

    it('should order the planets based on distance from the sun', async () => {
      const result = await homePageDataLoader();

      expect(result.planets[0].id).toBe('mercury');
      expect(result.planets[1].id).toBe('earth');
      expect(result.planets[2].id).toBe('mars');
    })
  })

  describe('when the api responds unsuccessfully', () => {
    beforeEach(() => {
      fetchMocker.mockReject(new Error('Failed to fetch'));
    })

    it('should throw an error', async () => {
      await expect(async () => await homePageDataLoader()).rejects.toThrowError();
    })
  })
})