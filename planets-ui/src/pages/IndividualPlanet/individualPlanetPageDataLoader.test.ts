import { vi } from "vitest";
import createFetchMock from 'vitest-fetch-mock';
import { Planet, individualPlanetPageDataLoader } from "./individualPlanetPageDataLoader";
import { LoaderFunctionArgs } from "react-router-dom";

const fetchMocker = createFetchMock(vi);
fetchMocker.enableMocks();

vi.stubEnv('VITE_API_URL', 'https://mock-api.com');

describe('individualPlanetPageDataLoader', () => {
  const callDataLoader = (planetId?: string) => {
    return individualPlanetPageDataLoader({ params: { planetId } } as unknown as LoaderFunctionArgs)
  };

  describe('with no planet ID', () => {
    it('should throw an error', async () => {
      await expect(async () => await callDataLoader()).rejects.toThrowError();
    })
  })

  describe('when the api responds successfully', () => {
    const planet: Planet = {
      id: 'mars',
      name: 'Mars',
      imageUrl: 'https://space-images.com/mars',
      diameterDisplayValue: '12,345 km',
      distanceToSunDisplayValue: '50.83 million km',
      massDisplayValue: '1.123 x 10^21 kg',
    };

    beforeEach(() => {
      fetchMocker.mockResponse(JSON.stringify(planet));
    });

    it('should call the correct api endpoint', async () => {
      await callDataLoader('mars');

      expect(fetchMocker).toHaveBeenLastCalledWith('https://mock-api.com/Planets/mars');
    })

    it('should return the deserialised planet', async () => {
      const result = await callDataLoader('mars');

      expect(result).toEqual({ planet });
    })
  })

  describe('when the api responds unsuccessfully', () => {
    beforeEach(() => {
      fetchMocker.mockReject(new Error('Api call failed'));
    });

    it('should throw an error', async () => {
      await expect(async () => await callDataLoader('mars')).rejects.toThrow('Api call failed');
    })
  })
})